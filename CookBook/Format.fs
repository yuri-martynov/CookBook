module Format

open System
open Types

let private dimension (half, one, two, five) (x: float)  =
    match x with
    |0.25 -> "четверть " + two
    |0.5 -> "пол" + half
    |1.0 -> "1 " + one
    |s when s > 1.0 && s < 5.0 -> sprintf "%s %s" (s.ToString("0.#")) two
    |s -> sprintf "%s %s" (s.ToString("0.#")) five

let private items = dimension ("штуки" ,"штука", "штуки" ,"штук")
let private tableSpoons = dimension ("cстоловой ложки", "столовая ложка", "столовых ложки" ,"столовых ложек")
let private teaSpoons = dimension (" чайной ложки", "чайная ложка", "чайных ложки" ,"чайных ложек")
let private liters = dimension ("-литра","литр","литра","литров")
let private grams = dimension ("грамма", "грамм","грамма","граммов")
let private seconds = dimension ("секунды", "секунду", "секунды", "секунд")
let private hours = dimension ("часа", "час", "часа", "часов")
let private minutes = dimension ("минуты", "минуту","минуты","минут")
let private glasses = dimension ("стакана", "стакан","стакана","стаканов")

let list (separator: string) (lst: seq<_>) : string =
    System.String.Join(separator, lst)

let quantity  = function 
    | Value { value = x; unit = u } ->
        match u with
        |Items -> x |> items
        |TableSpoons -> x |> tableSpoons
        |Liters -> x |> liters
        |Grams -> float x |> grams
        |Glasses -> x |> glasses
    | ToTaste -> "по вкусу"

let rec private product (x: Product) : string =
    match x with
    |Whole p -> p
    |Part (p, part) -> sprintf "%s (%s)" (product p) part


let private productQuantity (x : ProductQuantity) =
    sprintf "%s - %s" (product (x.product))  (quantity x.quantity)

let ingredient (x: Ingredient) =
    let many = Seq.map Utils.productsQuantities >> Seq.concat >> Seq.map productQuantity
    match x with
    |Only x -> productQuantity x
    |Optional x -> "можно добавить " + productQuantity x
    |Xor xs -> xs |> many |> list " либо "
    |And xs -> xs |> many |> list ". "



let duration (x: TimeSpan) =
    match x.TotalMinutes with
    | 30.0 -> "полчаса"
    | 60.0 -> "1 час"
    | m when m < 1.0 -> x.TotalSeconds |> seconds
    | m when m >= 120.0 -> x.TotalHours |> hours
    | x -> x |> minutes
    

let step (x: Step) =
    x.description + " - " + (x.duration |> duration)



    
    