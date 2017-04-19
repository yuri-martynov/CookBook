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
let private glasses = dimension ("стакана", "стакан","стакана","стаканов")

let list (separator: string) (lst: seq<_>) : string =
    System.String.Join(separator, lst)

let quantity (x : Quantity) =
    match x with
    |Items x -> x |> items
    |TableSpoons x -> x |> tableSpoons
    |Liters x -> x |> liters
    |Grams x -> float x |> grams
    |Glasses x -> x |> glasses
    |ToTaste -> "по вкусу"

let rec private product (x: Product) : string =
    match x with
    |Whole p -> p
    |Part (p, part) -> sprintf "%s (%s)" (product p) part


let private productQuantity (x : ProductQuantity) =
    sprintf "%s - %s" (product (x.product))  (quantity x.quantity)

let ingredient (x: Ingredient) =
    let many = Seq.map Utils.products >> Seq.concat >> Seq.map productQuantity
    match x with
    |Only x -> productQuantity x
    |Optional x -> "можно добавить " + productQuantity x
    |Xor xs -> xs |> many |> list " либо "
    |And xs -> xs |> many |> list "<br/>"



let duration (x: TimeSpan) =
    match x.TotalMinutes with
    | 30.0 -> "полчаса"
    | 60.0 -> "1 час"
    | m when m < 1.0 -> x.TotalSeconds |> seconds
    | m when m >= 120.0 -> x.TotalHours |> hours
    | x -> x |> minutes
    


let step (x: Step) =
    match x with
    | Manual x ->
        x.description + " - " + (x.duration |> duration)
    | Process x -> 
        sprintf "%s %s - %s" x.action x.item (x.duration |> duration)



    
    