module Format

open System.Collections
open Types


let entity (x: obj) : string =

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

let list (separator: string) (lst: IEnumerable) : string =
    System.String.Join(separator, lst)
    
    