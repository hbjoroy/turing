module Turing.Tape

    

type Tape = {
    Left: Symbol list
    Current: Symbol
    Right: Symbol list
}

let blank = '_'

let intializeTape (input: string) : Tape =
    let cells = input.ToCharArray() |> List.ofArray
    {
        Left = []
        Current = if List.isEmpty cells then blank else List.head cells
        Right = if List.isEmpty cells then [] else List.tail cells
    }

let moveLeft (tape: Tape) : Tape =
    match tape.Left with
    | [] ->
        {
            Left = []
            Current = blank
            Right = tape.Current :: tape.Right
        }
    | head :: tail ->
        {
            Left = tail
            Current = head
            Right = tape.Current :: tape.Right
        }

let moveRight (tape: Tape) : Tape = 
    match tape.Right with
    | [] -> 
        {
            Left = tape.Left @ [tape.Current]
            Current = blank
            Right = []
        }
    | head :: tail ->
        {
            Left = tape.Left @ [tape.Current]
            Current = head
            Right = tail
        }
let write (tape: Tape) (value: Symbol) : Tape =
    {
        Left = tape.Left
        Current = value
        Right = tape.Right
    }
let read (tape: Tape) : Symbol =
    tape.Current

let toList (tape: Tape) : Symbol list =
    tape.Left @ [tape.Current] @ tape.Right
