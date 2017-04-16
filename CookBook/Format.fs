module Format

open Types

let format (x: obj) =

    let quantity (x : Quantity) =
        match x with
        |Spoons s -> s.ToString() + " ложек"
        |x -> x.ToString()

    let product (x: Product) =
        sprintf "%s %s" x.name (quantity x.quantity)

    match x with
    | :? Product as x -> product x
    | :? Quantity as x -> quantity x
    |_ -> x.ToString()