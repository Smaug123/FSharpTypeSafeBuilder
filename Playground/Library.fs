namespace Playground

type Thing<'Input, 'Output> = {
    Name: string
    Description: string
    Supported: 'Input -> bool
    Execute: 'Input -> 'Output
}

type ThingBuilder<'Input, 'Output, 'Description, 'Supported, 'Execute> =
    internal
        {
            Name: string
            Description: 'Description
            Supported: 'Supported
            Execute: 'Execute
        }

[<RequireQualifiedAccess>]
module ThingBuilder =
    let toThing<'Input, 'Output> (builder : ThingBuilder<'Input, 'Output, string, 'Input -> bool, 'Input -> 'Output>) : Thing<'Input, 'Output> =
        {
            Name = builder.Name
            Description = builder.Description
            Supported = builder.Supported
            Execute = builder.Execute
        }

    let newThing<'Input, 'Output> (name : string) : ThingBuilder<'Input, 'Output, unit, unit, unit> =
        {
            Name = name
            Description = ()
            Supported = ()
            Execute = ()
        }

    let withDescription<'I, 'O, 'D1, 'D2, 'S, 'E> (desc : 'D2) (t : ThingBuilder<'I, 'O, 'D1, 'S, 'E>) =
        {
            Name = t.Name
            Description = desc
            Supported = t.Supported
            Execute = t.Execute
        }

    let withSupported<'I, 'O, 'D, 'S1, 'S2, 'E> (supp : 'S2) (t : ThingBuilder<'I, 'O, 'D, 'S1, 'E>) =
        {
            Name = t.Name
            Description = t.Description
            Supported = supp
            Execute = t.Execute
        }

    let withExecute<'I, 'O, 'D, 'S, 'E1, 'E2> (exec : 'E2) (t : ThingBuilder<'I, 'O, 'D, 'S, 'E1>) =
        {
            Name = t.Name
            Description = t.Description
            Supported = t.Supported
            Execute = exec
        }


module Example =
    let thing: Thing<string, string> =
        ThingBuilder.newThing "My thing"
        |> ThingBuilder.withExecute (fun (input: string) -> 42)
        |> ThingBuilder.withSupported (fun (input: string) -> true)
        |> ThingBuilder.withDescription "A description of my thing"
        |> ThingBuilder.withExecute (fun (input: string) -> input.Substring(0, 3))
        |> ThingBuilder.toThing