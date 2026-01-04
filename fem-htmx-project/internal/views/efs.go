package views

import "embed"

//go:embed "assets"
var Assets embed.FS

//go:embed "*"
var HTMLFiles embed.FS
