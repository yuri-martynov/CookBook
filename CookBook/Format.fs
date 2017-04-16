module Format

open System
open Types

let private dimension (half, one, two, five) (x: float)  =
    match x with
    |0.5 -> "пол" + half
    |1.0 -> "1 " + one
    |s when s > 1.0 && s < 5.0 -> s.ToString() + " " + two
    |s -> s.ToString() + " " + five

let private items = dimension ("штуки" ,"штука", "штуки" ,"штук")
let private tableSpoons = dimension (" столовой ложки", "столовая ложка", "столовых ложки" ,"столовых ложек")
let private liters = dimension ("-литра","литр","литра","литров")
let private grams = dimension ("грамма", "грамм","грамма","граммов")
let private seconds = dimension ("секунды", "секунду", "секунды", "секунд")
let private hours = dimension ("часа", "час", "часа", "часов")
let private minutes = dimension ("минуты", "минуту","минуты","минут")

let quantity (x : Quantity) =
    match x with
    |Items x -> x |> items
    |TableSpoons x -> x |> tableSpoons
    |Liters x -> x |> liters
    |Grams x -> float x |> grams
    |ToTaste -> "по вкусу"

let ingredient (x: Ingredient) =
    sprintf "%s - %s" x.product.name (quantity x.quantity)


let duration (x: TimeSpan) =
    match x.TotalMinutes with
    | 30.0 -> "полчаса"
    | 60.0 -> "1 час"
    | m when m < 1.0 -> x.TotalSeconds |> seconds
    | m when m >= 120.0 -> x.TotalHours |> hours
    | x -> x |> minutes
    

let step (x: Step) =
    x.description + " - " + (x.duration |> duration)


let list (separator: string) (lst: seq<_>) : string =
    System.String.Join(separator, lst)
    
    