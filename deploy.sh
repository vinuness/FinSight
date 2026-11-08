#!/bin/bash

set -e

echo "Atualizando código..."
git add .
git commit -m "*"
git push origin main

echo "Construindo containers..."
sudo docker compose build

echo "Subindo aplicação..."
sudo docker compose up -d

echo "Deploy concluído!"