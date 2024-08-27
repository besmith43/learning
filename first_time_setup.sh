#!/usr/bin/env bash

if [ ! -d /etc/ansible ]; then
	sudo mkdir -p /etc/ansible
fi

if [ ! -f /etc/ansible/hosts ]; then
	sudo touch /etc/ansible/hosts 
fi

sudo bash -c 'echo [servers] >> /etc/ansible/hosts'
sudo bash -c 'echo localhost ansible_connection=local >> /etc/ansible/hosts'

