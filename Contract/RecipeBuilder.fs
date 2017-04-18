module RecipeBuilder

open System
open Types

let private (@@) sequence item = 
    (sequence |> Seq.toList) @ [item]
    
let private (-@) sequence item =
    let cnt = sequence |> Seq.length
    (sequence |> Seq.take (cnt - 1) |> Seq.toList) @ [item]

let private lastStep dish =
    dish.recipe.steps |> Seq.last
    
let private replaceLastStep step dish  =
    { dish with recipe = { dish.recipe with steps = dish.recipe.steps -@ step } }
    
// Utils

let private toType<'t> (o : obj) : 't =
    Convert.ChangeType(o, typeof<'t>) :?> 't
    
// Time --------------------    
 
type TimeUnit =
    | Second
    | Minute
    | Hour
    
let private toTime value unit =
    let v = value |> toType<float>
    match unit with
    | Second -> v |> TimeSpan.FromSeconds
    | Minute -> v |> TimeSpan.FromMinutes
    | Hour -> v |> TimeSpan.FromHours
    
let [minute; minutes; минута; минуты; минут ] = Minute |> List.replicate 5 
let [hour; hours; час; часа; часов ] = Minute |> List.replicate 5 

// Quantity ------------------------

type QuantityUnit =
    | Item
    | Kg
    | Gram
    | Liter
    | Glass
    | TeeSpoon
    | TableSpoon
    | Taste
    
let [liter; liters; литр; литра; литров] = Liter |> List.replicate 5
let [table_spoon; table_spoons; столовой_ложки; столовая_ложка; столовые_ложки; столовых_ложек] = Liter |> List.replicate 6
let [item; items; штука; штуки; штук] = Liter |> List.replicate 5

let private toQuantity value unit : Quantity =
    match unit with
    | Item -> Items (toType<float> value)
    | Kg -> Grams ((toType<int> value) * 1000)
    | Gram -> Grams (toType<int> value)
    | Liter -> Liters (toType<float> value)
    | Glass -> Glasses (toType<float> value)
    | TeeSpoon -> TeaSpoons (toType<float> value)
    | TableSpoon -> TableSpoons (toType<float> value)
    | Taste -> ToTaste

// Builder --------------------------

type RecipeBuilder() =

    member __.Yield(()) : Dish =
        { name = ""
        ; recipe = { steps = [] }
        }
        
    [<CustomOperation("dish")>]
    member __.Dish (dish, name) =
        { dish with name = name }
        
    [<CustomOperation("step")>]
    member __.Step (dish, description) =
        let step = { duration = TimeSpan.Zero; description = description; ingredients = [] }
        { dish with recipe = { dish.recipe with steps = dish.recipe.steps @@ step } }
        
    [<CustomOperation("time")>]
    member __.Time (dish, time, unit) =
        let step = dish |> lastStep
        let step' = { step with duration = toTime time unit }
        { dish with recipe = { dish.recipe with steps = dish.recipe.steps -@ step' } }

    [<CustomOperation("ingredient")>]
    member __.Ingredient (dish, product, quantity, unit) =
        let ingredient = Only {product = Whole product; quantity = toQuantity quantity unit}
        let step = dish |> lastStep
        let step' = { step with ingredients = step.ingredients @@ ingredient }
        dish |> replaceLastStep step'
    
    // по-русски
    [<CustomOperation("рецепт")>] member x.Dish_ru (dish, name) = x.Dish(dish, name)
    [<CustomOperation("шаг")>]    member x.Step_ru (dish, description) = x.Step (dish, description)  
    [<CustomOperation("время")>]  member x.Time_ru (dish, time, u) = x.Time (dish, time, u)  
    [<CustomOperation("состав")>] member x.Ingredient_ru (dish, p,q,u) = x.Ingredient (dish, p, q,u)  
        
        
let recipe = RecipeBuilder()

        

