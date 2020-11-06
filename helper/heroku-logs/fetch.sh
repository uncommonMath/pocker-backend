#!/bin/bash

heroku logs -a pocker-backend -n 1500 -d=web | grep -F 'app[web.1]' > "production-$(date -I)".log
