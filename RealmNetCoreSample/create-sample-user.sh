#!/bin/bash

curl -H 'Content-Type:application/json' -d '{"name":"Hoge","password":"Fuga"}' http://localhost:5001/api/user/create


# curl -H 'Content-Type:application/json' -H 'X-AuthToken:~~~~' http://localhost:5001/api/database/config