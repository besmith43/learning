# Problems


[x] missing assets/ in the final deploy 
    possible solution:
        [include_dir](https://dev.to/konstantin/bundle-frontend-with-axum-build-using-includedir-g8i)

    actual solution:
        had to update the dockerfile to copy in the assests and set the workdir to where the binary is

[x] cloudflare not working (getting 520 http error code)
    
    solution:
        had to add a cert for the domain besmith.dev that I'm using as an experiment
        to do this, I used the following:

        ```bash
            fly certs add besmith.dev
        ```
        then add the cname values to cloudflare
