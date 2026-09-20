#!/usr/bin/env -S uv run --script


# /// script
# requires-python = ">=3.14"
# dependencies = [
#     "mactoast>=0.1.1",
# ]
# ///

from mactoast import show_success, show_error, show_warning, show_info

def main() -> None:
    show_success("File saved!")


if __name__ == "__main__":
    main()
