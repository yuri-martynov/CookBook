namespace Types

open System

type Unit =
    | Items 
    | Grams 
    | Liters
    | Glasses 
    | TableSpoons

type ValueOfUnits = { value: float; unit: Unit }

type Quantity =
    | Value of ValueOfUnits
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
    { ingredients: Ingredient seq
    }

type StepKind =
    | Manual of ManualStep
    | Process

type Step = 
    { duration: TimeSpan
    ; description: string
    ; kind: StepKind
    }
    
type Recipe =
    { steps: Step seq
    }

type Dish = 
    { name: string
    ; recipe: Recipe
    }