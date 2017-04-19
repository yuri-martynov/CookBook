namespace Contract.Tests
open System
open NUnit.Framework

open Types
open RecipeBuilder

[<TestFixture>]
type RecipeBuilderTests() = 

    [<Test>]
    member x.BuildsRecipe() =
    
        let вскипятить_воду l = step {
            шаг     "вскипятить воду"
            время   (l * 2 ) минут
            состав  "вода" l литров
            состав  "соль" (l/5) столовой_ложки        
        }
    
        let dish = recipe {
        
            блюдо   "мороженка"
            
            шаг'     [вскипятить_воду 5]
        
            шаг     "заморозить воду"
            время   2 часа
            
            шаг     "растопить шоколад"
            время   1 минута
            состав  "шоколадка" 1 шт

            шаг     "полить шоколадом"
            время   1 минута
        }
        
        Assert.AreEqual("мороженка", dish.name)
        Assert.AreEqual(4, dish.recipe.steps |> Seq.length)
        
        //let step = dish.recipe.steps |> Seq.head 
        //Assert.AreEqual("вскипятить воду", step.description)
        //Assert.AreEqual(TimeSpan.FromMinutes 10.0, step.duration)
        //Assert.AreEqual(2, step.ingredients |> Seq.length)
        
        //let ingredient = step.ingredients |> Seq.head
        //Assert.AreEqual(Only {product = Whole "вода"; quantity = Liters 5.0}, ingredient )