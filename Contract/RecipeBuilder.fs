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
    
let private updateStep step dish  =
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
    
let [kg; кг] = Kg |> List.replicate 2
let [gram; г] = Gram |> List.replicate 2
let [liter; liters; литр; литра; литров] = Liter |> List.replicate 5
let [table_spoon; table_spoons; столовой_ложки; столовая_ложка; столовые_ложки; столовых_ложек] = Liter |> List.replicate 6
let [item; items; шт] = Item |> List.replicate 3

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

// Step Builder --------------------------

type StepBuilder() =

    member __.Yield(()) : Step =
        Manual
            { description = null
            ; duration = TimeSpan.Zero
            ; ingredients = Seq.empty
            }
        
    [<CustomOperation("step")>]
    member __.Step (Manual step, description) =
        Manual { step with description = description }

    [<CustomOperation("time")>]
    member __.Time (Manual step, time, unit) =
        Manual { step with duration = toTime time unit }
        
    [<CustomOperation("ingredient")>]
    member __.Ingredient (Manual step, product, quantity, unit) =
        let ingredient = Only {product = Whole product; quantity = toQuantity quantity unit}
        Manual { step with ingredients = step.ingredients @@ ingredient }
    
    // по-русски
    [<CustomOperation("шаг")>]    member x.Step_ru (a, b) = x.Step (a, b)  
    [<CustomOperation("время")>]  member x.Time_ru (a, b, c) = x.Time (a, b, c)  
    [<CustomOperation("состав")>] member x.Ingredient_ru (a, b,c,d) = x.Ingredient (a, b, c, d)  


// Recipe Builder --------

type RecipeBuilder() =

    let _sb = StepBuilder()

    member __.Yield(()) : Dish =
        { name = ""
        ; recipe = { steps = [] }
        }
        
    [<CustomOperation("dish")>]
    member __.Dish (dish, name) =
        { dish with name = name }
        
    [<CustomOperation("step")>]
    member __.Step (dish, description) =
        let step = _sb.Step(_sb.Yield(), description )
        { dish with recipe = { dish.recipe with steps = dish.recipe.steps @@ step } }
        
    [<CustomOperation("step'")>]
    member __.Step' (dish, steps) =
        { dish with recipe = { dish.recipe with steps = (dish.recipe.steps |> Seq.toList) @ steps } }
        
    [<CustomOperation("time")>]
    member __.Time (dish, time, unit) =
        let step = dish |> lastStep
        let step' = _sb.Time(step, time, unit)
        dish |> updateStep step'

    [<CustomOperation("ingredient")>]
    member __.Ingredient (dish, product, quantity, unit) =
        let step = dish |> lastStep
        let step' = _sb.Ingredient(step, product, quantity, unit)
        dish |> updateStep step'
    
    // по-русски
    [<CustomOperation("блюдо")>]    member x.Dish_ru (dish, name) = x.Dish(dish, name)
    [<CustomOperation("шаг")>]      member x.Step_ru (dish, description) = x.Step (dish, description)  
    [<CustomOperation("шаг'")>]     member x.Step'_ru (dish, step) = x.Step' (dish, step)  
    [<CustomOperation("время")>]    member x.Time_ru (dish, time, u) = x.Time (dish, time, u)  
    [<CustomOperation("состав")>]   member x.Ingredient_ru (dish, p,q,u) = x.Ingredient (dish, p, q,u)  
        
        
        
// Workflows
let recipe = RecipeBuilder()
let step = StepBuilder()

// по-русски
let рецепт = recipe
let шаг = step
