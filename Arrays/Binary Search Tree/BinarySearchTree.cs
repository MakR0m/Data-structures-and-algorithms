using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Binary_Search_Tree
{
    internal class BinarySearchTree
    {
        //Массивы и списки - линейные (есть начало и конец), деревья ветвятся. У дерева есть корень и глубина ветвей. В деревьях нельзя приходить к одному узлу из двух и более ветвей. Крайние элементы - это листья.
        //Бинарное дерево поиска - ограничение только два и менее ветвей из нода. Условие. все, что меньше нода вставляется слева, все что больше - справа.
        //Добавление не заменяет элемент, а продолжает встраивать в листья. Позволяет быстрее искать элементы.

        //Виды обхода дерева: префиксный (элемент, левое поддерево, правое поддерево), постфиксный (левое поддерево, правое поддерево, элемент), инфиксный (левое поддерево, элемент, правое поддерево)
        //Префиксный, подходит для копирования. Постфиксный подходит при удалении (всегда удаляем крайние). Инфиксный получили все элементы в порядке возрастания (сортировку).

        private TreeNode? root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void Insert(int value)
        {
            root = InsertRecursive(root, value);
        }

        private TreeNode InsertRecursive(TreeNode node, int value)
        {
            if (node == null)
                return new TreeNode(value);
            if (value < node.Value) 
                node.Left = InsertRecursive(node.Left, value);
            else
                node.Right = InsertRecursive(node.Right, value);
            return node;
        }



        public bool Contains(int value)
        {
            return ContainsRecursive(root,value);
        }

        private bool ContainsRecursive(TreeNode? node, int value)
        {
            if (node == null)
                return false;
            if (node.Value == value) return true;
            if (value < node.Value)
                return ContainsRecursive(node.Left, value);
            else
                return ContainsRecursive(node.Right, value);
        }

        public void PrintInOrder()
        {
            PrintInOrderRecursive(root);
            Console.WriteLine();
            
        }

        private void PrintInOrderRecursive(TreeNode? node)
        {
            if (node == null)
                return;
            PrintInOrderRecursive(node.Left);
            Console.Write($"{ node.Value} ");
            PrintInOrderRecursive(node.Right);

        }

        public void Remove(int value)
        {
            root = RemoveRecursive(root, value);
        }

        private TreeNode? RemoveRecursive(TreeNode? node, int value)
        {
            if (node == null)
                return null;

            if (value < node.Value)
            {
                node.Left = RemoveRecursive(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = RemoveRecursive(node.Right, value);
            }
            else
            {
                // Случай 1: нет детей
                if (node.Left == null && node.Right == null)
                    return null;

                // Случай 2: один потомок
                if (node.Left == null)
                    return node.Right;
                if (node.Right == null)
                    return node.Left;

                // Случай 3: два потомка
                // Найдём минимальное значение в правом поддереве
                TreeNode minNode = FindMin(node.Right);
                node.Value = minNode.Value;
                node.Right = RemoveRecursive(node.Right, minNode.Value);
            }

            return node;
        }

        private TreeNode FindMin(TreeNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }


    }
}
