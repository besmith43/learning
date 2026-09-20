#!/usr/bin/env -S uv run --script


# /// script
# requires-python = ">=3.14"
# dependencies = [
#     "macos-notifications>=0.2.1",
# ]
# ///


from mac_notifications import client


def main() -> None:
    client.create_notification(title="Meeting starts now!", subtitle="Team Standup")


if __name__ == "__main__":
    main()
