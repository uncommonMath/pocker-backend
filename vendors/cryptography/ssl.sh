#!/bin/bash

if [ -f ssl.pfx ]; then
    echo "ssl.pfx already exists. Exiting..."
    exit 1
else
    openssl req -x509 -newkey rsa:4096 -nodes -days 3650 -subj '/CN=.' -keyout ssl.key -out ssl.crt
    openssl pkcs12 -export -passout pass: -inkey ssl.key -in ssl.crt -out ssl.pfx
    openssl pkcs12 -info -passin pass: -in ssl.pfx -noout
fi
