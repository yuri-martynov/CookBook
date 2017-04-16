module Format

open System
open Types

let private dimension (half, one, two, five) (x: float)  =
    match x with
    |0.5 -> "пол" + half
    |1.0 -> "1 " + one
    |s when s > 1.0 && s < 5.0 -> s.ToString() + " " + two
    |s -> s.ToString() + " " + five

let quantity (x : Quantity) =
    match x with
    |Items x -> x |> dimension ("штуки" ,"штука", "штуки" ,"штук")
    |TableSpoons x -> x |> dimension (" столовой ложки", "столовая ложка", "столовых ложки" ,"столовых ложек")
    |Liters x -> x |> dimension ("-литра","литр","литра","литров")
    |Grams x -> float x |> dimension ("грамма", "грамм","грамма","граммов")

let ingredient (x: Ingredient) =
    sprintf "%s - %s" x.product.name (quantity x.quantity)


let duration (x: TimeSpan) =
    match x.TotalMinutes with
    | 30.0 -> "полчаса"
    | 60.0 -> "около часа"
    | m when m < 1.0 -> x.TotalSeconds |> dimension ("полсекунды", "секунду", "секунды", "секунд")
    | x -> x |> dimension ("полминуты", "минуту","минуты","минут")
    


let list (separator: string) (lst: seq<_>) : string =
    System.String.Join(separator, lst)
    
    