namespace Contract.Tests
open System
open NUnit.Framework

open Types
open RecipeBuilder

[<TestFixture>]
type RecipeBuilderTests() = 

    [<Test>]
    member x.BuildsRecipe() =
        let dish = recipe {
        
            рецепт "мороженка"
        
            шаг "вскипятить воду"
            время 10 минут
            состав "вода" 5 литров
            состав "соль" 0.5 столовой_ложки

            шаг "заморозить воду"
            время 2 часа
            
            шаг "растопить шоколад"
            время 1 минута
            состав "шоколадка" 1 штука

            шаг "полить шоколадом"
            время 0.5 минуты
        }
        
        Assert.AreEqual("мороженка", dish.name)
        Assert.AreEqual(4, dish.recipe.steps |> Seq.length)
        
        let step = dish.recipe.steps |> Seq.head 
        Assert.AreEqual("вскипятить воду", step.description)
        Assert.AreEqual(TimeSpan.FromMinutes 10.0, step.duration)
        Assert.AreEqual(2, step.ingredients |> Seq.length)
        
        let ingredient = step.ingredients |> Seq.head
        Assert.AreEqual(Only {product = Whole "вода"; quantity = Liters 5.0}, ingredient )
        
        
        

