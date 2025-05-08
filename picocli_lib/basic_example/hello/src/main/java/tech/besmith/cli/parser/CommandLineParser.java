package tech.besmith.cli.parser;

import tech.besmith.cli.commands.CreateCommand;
import tech.besmith.cli.commands.DeleteCommand;
import tech.besmith.cli.commands.ListCommand;
import tech.besmith.cli.commands.UpdateCommand;
import tech.besmith.cli.common.HelpOption;
import picocli.CommandLine.Command;
import picocli.CommandLine.Mixin;

import static picocli.CommandLine.Option;

@Command(
        subcommands = {CreateCommand.class, ListCommand.class, UpdateCommand.class, DeleteCommand.class},
        versionProvider = VersionProvider.class
)
public class CommandLineParser {

    @Mixin
    private HelpOption helpOption;
    @Option(names = {"-v", "--version"}, versionHelp = true, description = "Print version information and exit.")
    private boolean versionHelpRequested;
}
