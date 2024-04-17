#!/usr/bin/env python3

import argh
import os
import shutil
import threading
from rich.live import Live
from rich.panel import Panel
from rich.progress import Progress, SpinnerColumn, BarColumn, TextColumn
from rich.table import Table
from concurrent.futures import as_completed, ThreadPoolExecutor
from functools import partial
from threading import Event
import time

def get_files(wd):
    return [f for f in os.listdir(wd) if os.path.isfile(os.path.join(wd, f))]

def get_folders(wd):
    return [d for d in os.listdir(wd) if os.path.isdir(os.path.join(wd, d))]

def _copy(source, destination):
    tic = time.perf_counter()
    shutil.copyfile(source,os.path.join(destination, os.path.basename(source)))
    toc = time.perf_counter()

    print(f"file copy runtime: {toc-tic:0.4f} seconds")

    if os.path.exists(os.path.join(destination, os.path.basename(source))):
        return True
        
    return False

def single_file_copy(source, destination):
    print("running single file copy")
    print("Size of Source File: " + str(os.path.getsize(source)))
    prog_bar = Progress(
        "{task.description}",
        SpinnerColumn(),
        BarColumn(),
        TextColumn("[progress.percentage]{task.percentage:>3.0f}%"),
    )
    task_id = prog_bar.add_task("Copying " + os.path.basename(source), total=100)

    progress_table = Table.grid()
    progress_table.add_row(
            Panel.fit(
                    prog_bar,title="Single File Copy",border_style="blue"
                )
            )
    progress = 0
    with Live(progress_table, refresh_per_second=10):
        t = threading.Thread(name='copying',target=_copy, args=(source, destination))
        t.start()
        #while not os.path.exists(os.path.join(destination,os.path.basename(source))):
            #time.sleep(0.1)
        tic = time.perf_counter()
        while not prog_bar.finished:
            time.sleep(0.1)
            progress = os.path.getsize(os.path.join(destination,os.path.basename(source))) / os.path.getsize(source) * 100
            prog_bar.update(task_id, advance=progress)
        toc = time.perf_counter()
        print(f"prog bar runtime: {toc-tic:0.4f} seconds")

def folder_copy(source, destination):
    print("running folder copy")

def main(source,destination):
    if not os.path.exists(source):
        print("Source Directory or File given doesn't exist")
        exit(1)

    if not os.path.exists(destination):
        print("Destination Directory given doesn't exist")
        exit(1)
    elif not os.path.isdir(destination):
        print("Destination given wasn't a directory")
        exit(1)
    elif os.path.isfile(os.path.join(destination, os.path.basename(source))):
        print("File already exists at the destination")
        exit(1)

    print(source)
    print(destination)

    if os.path.isfile(source):
        single_file_copy(source, destination)
        exit(0)
    elif os.path.isdir(source):
        folder_copy(source, destination)
        exit(0)

if __name__ == "__main__":
    argh.dispatch_command(main)
