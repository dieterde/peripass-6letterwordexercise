# Peripass interview - Technical Developer test - v0.1
Created by Dieter Deriemaeker (dieter@ditsolution.be)

## Usage

Run the program with optional arguments for the input file.

```bash
dotnet run -- input.txt
```

## Possible Improvements

With more time, the following enhancements could be added:

- **Interfaces & DI:** abstract the infrastructure and domain classes behind interfaces, enabling dependency injection and reusable libraries.
- **Unit tests:** add test coverage.  
- **Parallel execution:** improve performance by processing targets concurrently and handling large input files more efficiently.
- **Configurable parameters:** read target length and other settings from command-line arguments or a configuration file.  