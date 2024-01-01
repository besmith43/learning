const { Command } = require("commander");
const figlet = require("figlet");
const fs = require("fs");
const path = require("path");

// this code came from here:
// https://blog.logrocket.com/building-typescript-cli-node-js-commander/

const program = new Command();

console.log(figlet.textSync("Dir Manager"));

program
    .version("1.0.0")
    .description("An example CLI for managing a directory")
    .option("-l, --ls  [value]", "List directory contents")
    .option("-m, --mkdir <value>", "Create a directory")
    .option("-t, --touch <value>", "Create a file")
    .parse(process.argv);

const options = program.opts();


// check if the option has been used the user
if (options.ls) {
  const filepath = typeof options.ls === "string" ? options.ls : __dirname;
  listDirContents(filepath);
}

//define the following function
async function listDirContents(filepath: string) {
    try {
        // add the following
        const files = await fs.promises.readdir(filepath);
        const detailedFilesPromises = files.map(async (file: string) => {
            let fileDetails = await fs.promises.lstat(path.resolve(filepath, file));
            const { size, birthtime } = fileDetails;
            return { filename: file, "size(KB)": size, created_at: birthtime };
        });

        const detailedFiles = await Promise.all(detailedFilesPromises);
        console.table(detailedFiles);

    } catch (error) {
        console.error("Error occurred while reading the directory!", error);
    }
}
