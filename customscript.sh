#! /bin/bash

### This script sets up the ubuntu server for keycloak ###
### It uses nginx for ssl offloading of the keycloak traffic ###
### 
###
### 1. Initial setup - package installations
### 2. Install keycloak as a docker container
### 3. Generating the certificates for NGINX configuration
### 4. NGINX Configuration file
### 5. Setting up NGINX
###

# Initial Setup

sudo apt-get -y update
sudo apt-get -y install \
    ca-certificates \
    curl \
    gnupg \
    lsb-release
sudo mkdir -p /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get -y update
sudo apt-get -y install docker-ce docker-ce-cli containerd.io docker-compose-plugin
sudo systemctl start docker
sudo systemctl enable docker

# Install nginx
sudo apt-get -y install nginx

# Install keycloak as a docker container
sudo docker run -d -p 8080:8080 -e KEYCLOAK_USER=admin -e KEYCLOAK_PASSWORD=pass -e PROXY_ADDRESS_FORWARDING=true jboss/keycloak

# Azure cli
sudo apt-get -y update
sudo apt-get -y install ca-certificates curl apt-transport-https lsb-release gnupg
curl -sL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor | sudo tee /etc/apt/trusted.gpg.d/microsoft.gpg > /dev/null
AZ_REPO=$(lsb_release -cs)
echo "deb [arch=amd64] https://packages.microsoft.com/repos/azure-cli/ $AZ_REPO main" | sudo tee /etc/apt/sources.list.d/azure-cli.list
sudo apt-get -y update
sudo apt-get -y install azure-cli

# Download certificate from azure blob storage
sudo az storage blob download --file /etc/nginx/cert.key --name private.key --container-name certificate --auth-mode key --account-name useast2saccgeneralthings --account-key QKi15Z1eHtsrAei/0qinqETOCNT0LIMZSgRI33lCyeeyXCtKNZwf4eQASprkJ9uxDr7OzqaYNKIa+ASt4Dv6LA==
sudo az storage blob download --file /etc/nginx/cert.crt --name certificate.crt --container-name certificate --auth-mode key --account-name useast2saccgeneralthings --account-key QKi15Z1eHtsrAei/0qinqETOCNT0LIMZSgRI33lCyeeyXCtKNZwf4eQASprkJ9uxDr7OzqaYNKIa+ASt4Dv6LA==

echo $(pwd)

ls -l
# NGINX Configuration
{
cat > nginxconfig.conf << EOF
server {
    
    listen 80;
    listen 443 default ssl;
    server_name keycloak.sobreiro.dev;

    ssl_certificate           /etc/nginx/cert.crt;
    ssl_certificate_key       /etc/nginx/cert.key;

    ssl_session_cache  builtin:1000  shared:SSL:10m;
    ssl_protocols  TLSv1 TLSv1.1 TLSv1.2;
    ssl_ciphers HIGH:!aNULL:!eNULL:!EXPORT:!CAMELLIA:!DES:!MD5:!PSK:!RC4;
    ssl_prefer_server_ciphers on;

    access_log            /var/log/nginx/jenkins.access.log;

    location / {
      proxy_set_header        Host \$host;
      proxy_set_header        X-Real-IP \$remote_addr;
      proxy_set_header        X-Forwarded-For \$remote_addr;
      proxy_set_header        X-Forwarded-Proto \$scheme;

      # Fix the “It appears that your reverse proxy set up is broken" error.
      proxy_pass          http://localhost:8080;
      proxy_read_timeout  90;

      proxy_redirect      http://localhost:8080 https://keycloak.cudanet.com;
      
    }
  }
EOF
}
sudo cp nginxconfig.conf nginxconfig.conf.bak
sudo mv nginxconfig.conf /etc/nginx/sites-enabled/default
sudo systemctl enable nginx
sudo systemctl stop nginx
sudo systemctl start nginx