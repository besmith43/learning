# Agent Instructions

Keep IO operations separate from compute operations.

IO includes file reads and writes, user input, network calls, database calls, environment access, and any other interaction with external state.

Compute should be pure logic: deterministic code that receives all required data as arguments and returns a value describing the result or next action.

When implementing behavior:

1. Gather all required input through IO at the boundary of the program or workflow.
2. Pass that data into pure compute functions.
3. Have compute functions return structured results, decisions, or commands.
4. Perform any follow-up IO only after compute has returned.

Prefer this shape:

```text
IO layer:
  read inputs
  call compute function
  apply returned actions

Compute layer:
  accept data
  perform validation/checks/transforms
  return result without doing IO
```

Avoid mixing file reads, prompts, network requests, or database access inside logic that should be unit tested.
