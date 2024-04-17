#!/bin/bash


cat weatherforecast.json | jq '.'


cat weatherforecast.json | jq '.[]'


cat weatherforecast.json | jq '.location'


cat weatherforecast.json | jq '.date'



cat weatherforecasts.json | jq '.[]' | jq '.location'
