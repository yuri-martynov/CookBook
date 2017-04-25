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
    
let (sec, сек ) = (Second, Second) 
let (minute, minutes, мин ) = (Minute, Minute, Minute) 
let (hour, hours, ч) = (Hour, Hour, Hour) 

// Quantity ------------------------

type RecipeBuilderUnit =
    | Unit of Unit
    | Kg
    | TeaSpoon
    
let (kg, кг) = (Kg, Kg)
let (g, г) = (Unit Grams, Unit Grams)
let (liter, liters, л) = (Unit Liters, Unit Liters, Unit Liters) 
let (table_spoon, table_spoons, ст_л) = (Unit TableSpoons, Unit TableSpoons, Unit TableSpoons) 
let (item, items, шт) = (Unit Items, Unit Items, Unit Items) 
let (glass, glasses, стакан, стакана) = (Unit Glasses, Unit Glasses, Unit Glasses, Unit Glasses) 

let private toQuantity (value: obj) (unit: RecipeBuilderUnit) : Quantity =
    match unit with
    | Unit u -> Value { value = value |> toType<float> ; unit = u}
    | Kg -> Value { value =  (toType<float> value) * 1000.0; unit = Grams }
    | TeaSpoon -> Value { value =  (toType<float> value) * 3.0; unit = TableSpoons }

// Step Builder --------------------------

type ManualStepBuilder() =

    member __.Yield(()) : Step =
        Manual
            { description = null
            ; duration = TimeSpan.FromSeconds 10.0
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
        let ingredient = Only {product =  product; quantity = toQuantity quantity unit}
        Manual { step with ingredients = step.ingredients @@ ingredient }

    [<CustomOperation("to_taste")>]
    member __.ToTaste (Manual step, product) =
        let ingredient = Only {product =  product; quantity = ToTaste }
        Manual { step with ingredients = step.ingredients @@ ingredient }
    
    // по-русски
    [<CustomOperation("шаг")>]    member x.Step_ru (a, b) = x.Step (a, b)  
    [<CustomOperation("время")>]  member x.Time_ru (a, b, c) = x.Time (a, b, c)  
    [<CustomOperation("состав")>] member x.Ingredient_ru (a, b,c,d) = x.Ingredient (a, b, c, d)  
    [<CustomOperation("по_вкусу")>] member x.ToTaste_ru (a, b) = x.ToTaste (a, b)  


// Recipe Builder --------

type RecipeBuilder() =

    let _sb = ManualStepBuilder()

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

    member __.Step (dish, step : Step) =
        { dish with recipe = { dish.recipe with steps = dish.recipe.steps @@ step } }

    member __.Step (dish, steps : Step list) =
        { dish with recipe = { dish.recipe with steps = (dish.recipe.steps |> Seq.toList) @ steps } }

    [<CustomOperation("add")>]
    member __.Add(dish, product, q, u) =
        let step = 
            Manual 
                { description = "добавить " + (product |> Utils.productName)
                ; duration = TimeSpan.FromSeconds 10.0 
                ; ingredients = [ Only {product = product; quantity = toQuantity q u }]
                }
        { dish with recipe = { dish.recipe with steps = dish.recipe.steps @@ step } }

        
        
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

    [<CustomOperation("to_taste")>]
    member __.ToTaste (dish, product) =
        let step = dish |> lastStep
        let step' = _sb.ToTaste(step, product)
        dish |> updateStep step'

    [<CustomOperation("process")>]
    member __.Process (dish, description, time, unit) =
        let step = Process { description = description; duration = toTime time unit }
        { dish with recipe = { dish.recipe with steps = dish.recipe.steps @@ step } }
    
    // по-русски
    [<CustomOperation("блюдо")>]    member x.Dish_ru (dish, name) = x.Dish(dish, name)
    [<CustomOperation("шаг")>]      member x.Step_ru (dish, description: string) = x.Step (dish, description)  
    [<CustomOperation("время")>]    member x.Time_ru (dish, time, u) = x.Time (dish, time, u)  
    [<CustomOperation("состав")>]   member x.Ingredient_ru (dish, p,q,u) = x.Ingredient (dish, p, q,u)  
    [<CustomOperation("процесс")>]  member x.Process_ru (dish, p,q,u) = x.Process (dish, p, q,u)  
    [<CustomOperation("по_вкусу")>] member x.ToTaste_ru (a, b) = x.ToTaste (a, b)  
    [<CustomOperation("добавить")>] member x.Add_ru (a, b,c,d) = x.Add (a, b,c,d)  
        
        
        
// Workflows
let recipe = RecipeBuilder()
let step = ManualStepBuilder()

// по-русски
let рецепт = recipe
let шаг = step

