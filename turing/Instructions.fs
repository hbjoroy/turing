module Turing.Instructions

type Movement =
    | Left
    | Right
    | Stay

type Transition = {
    current: Symbol
    write: Symbol
    newStateName: string
    movement: Movement
}

let toTransition tupleList = 
    let doMap  (current, write, newStateName, movement) = 
        { current = current; write = write; newStateName = newStateName; movement = movement }
    tupleList
    |> List.map doMap

type State = {
    name: string
    transitions: Transition list
}

type Program = {
    states: State list
    initialStateName: string
    finalStateNames: string list
}

