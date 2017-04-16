module Format

open System.Collections
open Types

let quantity (x : Quantity) =
    match x with
    |Spoons s -> s.ToString() + " ложек"
    |x -> x.ToString()

let product (x: Product) =
    sprintf "%s %s" x.name (quantity x.quantity)


let list (separator: string) (lst: seq<_>) : string =
    System.String.Join(separator, lst)
    
    