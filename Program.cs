using InputKeyboard;
using Menu;
using Lab_10lib;
using BinarySearchTree;
using Lab12;
using System.Collections;

namespace Xeosha
{
    public class Program
    {
        public static Goods[] GenerateRandomGoodsArray(int size)
        {
            Goods[] products = new Goods[size];
            var rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                int item = rnd.Next(1, 5);
                products[i] = CreateProduct(item);
            }

            return products;
        }

        public static Goods CreateProduct(int itemType)
        {
            return itemType switch
            {
                1 => new Goods(),
                2 => new Toy(),
                3 => new Product(),
                4 => new MilkProduct(),
                _ => throw new ArgumentException("Неизвестный тип!"),
            };
        }

        static Goods[] CreateRandomGoodsArray()
        {
            int size = EnterKeybord.TypeInteger("Введите размер массива: ", 0);
            return GenerateRandomGoodsArray(size);
        }


        static Goods GetMenuTypeProducts(int position)
        {
            var goods = new Goods();
            var dialog = new Dialog($"Выберите тип товара для {position} позиции. Если создали, нажмите ESC. Если не выберите - рандом.");
            dialog.AddOption("Базовый класс (Товар)", () => { goods = new Goods(); goods.Init(); dialog.Close(); });
            dialog.AddOption("Игрушка", () => { goods = new Toy(); goods.Init(); dialog.Close(); });
            dialog.AddOption("Продукт", () => { goods = new Product(); goods.Init(); dialog.Close(); });
            dialog.AddOption("Молочный продукт", () => { goods = new MilkProduct(); goods.Init(); dialog.Close(); });
            dialog.Start();

            Console.WriteLine("Созданный товар:\n" + goods);
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey(true);

            return goods;
        }

        static Goods[] CreateKeyboardGoodsArray()
        {
            int size = EnterKeybord.TypeInteger("Введите размер массива: ", 0);
            Goods[] products = new Goods[size];
            for (int i = 0; i < size; i++)
            {
                products[i] = GetMenuTypeProducts(i + 1);

            }

            return products;
        }
        static Goods[] CreateArray()
        {
            Goods[] goods = Array.Empty<Goods>();

            var dialog = new Dialog("Создание массива товаров");
            dialog.AddOption("Рандомное создание", () => { goods = CreateRandomGoodsArray(); dialog.Close("Список товаров создан."); });
            dialog.AddOption("Создание с клавиатуры", () => { goods = CreateKeyboardGoodsArray(); dialog.Close("Список товаров создан."); }, true);
            dialog.Start();

            return goods;
        }


        public static void AddProductInTree(BinaryTree<Goods> tree)
        {
            tree.AddRange(CreateArray());
        }


        public static void TaskClone(BinaryTree<Goods> tree)
        {
            if (tree.Count == 0)
            {
                var products = GenerateRandomGoodsArray(5);
                tree = new BinaryTree<Goods>(products, new CustomComparer());
            }

            Console.WriteLine("\tИзначальное дерево: ");
            tree.PrintTree();
            foreach (var item in tree)
                Console.WriteLine(item);

            Console.WriteLine("\tПоверхностное копирование дерева: ");
            ((BinaryTree<Goods>)tree.ShallowCopy()).PrintTree();

            Console.WriteLine("\tГлубокое копирование дерева: ");
            ((BinaryTree<Goods>)tree.Clone()).PrintTree();
        }

        static void ShowTree(BinaryTree<Goods> tree)
        {
            Console.WriteLine("\tКоличество товаров в дереве поиска: " + tree.Count);

            Console.WriteLine();

            Console.WriteLine("\tДерево поиска через PrintTree:");
            tree.PrintTree();

            Console.WriteLine();

            Console.WriteLine("\tIEnumerable:");
            foreach (var item in tree)
                Console.WriteLine(item + "\n---");
        }

        static void DeleteItemTree(BinaryTree<Goods> tree)
        {
            foreach (var item in CreateArray())
                tree.Remove(item);
        }

        static void FindItem(BinaryTree<Goods> tree)
        {
            var product = GetMenuTypeProducts(0);
            var findProduct = tree.Find(product);
            Console.Clear();
            Console.WriteLine("\tНайденный продукт: ");
            if (findProduct != null)
                Console.WriteLine(findProduct);
            else
                Console.WriteLine("Товар не найден");
            //uiii
        }

        public static void fuction()
        {
            var tree = new BinaryTree<CloneableInt>();

            tree.AddRange(new List<CloneableInt> { 8, 7, 3, 1, 5, 6, 10});
            tree.Remove(1);

            Console.WriteLine("\tКоличество элементов: " + tree.Count);

            tree.PrintTree();

            Console.ReadKey(true);
        }
        public static void Main()
        {
            fuction();
            // Компаратор по цене.
            var searchTree = new BinaryTree<Goods>(new CustomComparer());

            var dialog = new Dialog("12-ая Лабораторная работа");
            dialog.AddOption("Добавление нового элемента (ов) ", () => AddProductInTree(searchTree), true);
            dialog.AddOption("Удаление элемента (ов) ", () => DeleteItemTree(searchTree), true);
            dialog.AddOption("Вывод дерева (+ IEnumerable)", () => ShowTree(searchTree));
            dialog.AddOption("Поиск элемента", () => FindItem(searchTree));
            dialog.AddOption("Удалить дерево из памяти", () => { searchTree.Clear(); });
            dialog.AddOption("Клон дерева", () => TaskClone(searchTree));
            dialog.Start();


        }

    }
}