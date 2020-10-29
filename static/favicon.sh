#!/bin/bash

#requre imagemagick
convert -resize x16 -gravity center -crop 16x16+0+0 favicon.png -background transparent favicon.ico