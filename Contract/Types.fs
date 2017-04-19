namespace Types

open System

type Quantity =
    | Items of float
    | Grams of int
    | Liters of float
    | Glasses of float
    | TeaSpoons of float
    | TableSpoons of float
    | ToTaste
    

type Product =
    | Whole of string
    | Part of (Product * string)

type ProductQuantity =
    { product: Product
    ; quantity: Quantity
    }

type Ingredient =
    | Only of ProductQuantity
    | Optional of ProductQuantity
    | Xor of Ingredient seq
    | And of Ingredient seq


type ManualStep =
    { duration: TimeSpan
    ; description: string
    ; ingredients: Ingredient seq
    }

type ProcessStep =
    { duration: TimeSpan
    ; action: string
    ; item: string
    }

type Step =
    | Manual of ManualStep
    | Process of ProcessStep
    
type Recipe =
    { steps: Step seq
    }

type Dish = 
    { name: string
    ; recipe: Recipe
    }