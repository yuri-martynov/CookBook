module Format

open System.Collections
open Types

let quantity (x : Quantity) =
    
    let dimension (half, one, two, five) x  =
        match x with
        |0.5 -> "1/2 " + half
        |1.0 -> "1 " + one
        |s when s > 1.0 && s < 5.0 -> sprintf "%f %s " s two
        |s -> sprintf "%f %s " s five


    match x with
    |Items x -> x |> dimension ("штуки" ,"штука", "штуки" ,"штук")
    |TableSpoons x -> x |> dimension ("столовой ложки", "столовая ложка", "столовых ложки" ,"столовых ложек")
    |Liters x -> x |> dimension ("литра","литр","литра","литров")
    |Grams x -> x |> dimension ("грамма", "грамм","грамма","граммов")

let ingredient (x: Ingredient) =
    sprintf "%s - %s" x.product.name (quantity x.quantity)


let list (separator: string) (lst: seq<_>) : string =
    System.String.Join(separator, lst)
    
    