namespace Turing

type Symbol = char

type Status<'a> =
    | Running of 'a
    | Halted of 'a
    | Failed of 'a
    | Finished of 'a