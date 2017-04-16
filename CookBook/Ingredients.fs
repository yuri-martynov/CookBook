module Ingredients

let get id : Async<string> = 
    
    let mapProduct (product : Data.DishXml.Product) =
        product.Value + " "  + product.Quantity
        

    async {
        let! dish = Data.get id
        return dish.Ingredients.Products 
            |> Seq.map mapProduct
            |> fun products -> System.String.Join("<br/>", products)
    }

