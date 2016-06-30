package main

import (
	"fmt"

	"github.com/valyala/fasthttp"
)

func main() {
	fasthttp.ListenAndServe(":9292", func(ctx *fasthttp.RequestCtx) {
		fmt.Fprintf(ctx, "Hello World")
	})
}

