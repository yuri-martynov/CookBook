module Format

open System.Collections
open Types

let quantity (x : Quantity) =
    
    let dimension (half, one, two, five) x  =
        match x with
        |0.5 -> "1/2 " + half
        |1.0 -> "1 " + one
        |s when s > 1.0 && s < 5.0 -> s.ToString() + " " + two
        |s -> s.ToString() + " " + five


    match x with
    |Items x -> x |> dimension ("штуки" ,"штука", "штуки" ,"штук")
    |Spoons x -> x |> dimension ("столовой ложки", "столовая ложка", "столовых ложки" ,"столовых ложек")
    |Liters x -> x |> dimension ("литра","литр","литра","литров")
    |Gramm x -> x |> dimension ("грамма", "грамм","грамма","граммов")
    |x -> x.ToString()

let product (x: Product) =
    sprintf "%s - %s" x.name (quantity x.quantity)


let list (separator: string) (lst: seq<_>) : string =
    System.String.Join(separator, lst)
    
    