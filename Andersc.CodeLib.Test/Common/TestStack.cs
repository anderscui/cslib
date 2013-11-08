using System;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Tester.Common
{
    [TestFixture]
    public class TestStack
    {
        private Stack<int> GetEmptyStack()
        {
            return new Stack<int>();
        }

        private Stack<int> GetStackRangeOneToTen()
        {
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i + 1);
            }

            return stack;
        }

        [Test]
        public void TestIsEmpty()
        {
            Stack<int> stack = GetEmptyStack();
            Assert.That(stack, Is.Empty);

            stack.Push(1);
            Assert.That(stack, Is.Not.Empty);

            Stack<int> stack2 = new Stack<int>(10);
            Assert.That(stack2, Is.Empty);

            stack2.Push(1);
            Assert.That(stack2, Is.Not.Empty);

            int[] array = { 1, 2, 4, 8 };
            Stack<int> stack3 = new Stack<int>(array);
            Assert.That(stack3, Is.Not.Empty);
            Assert.That(stack3.Top, Is.EqualTo(array[array.Length - 1]));
            Assert.That(stack3.Count, Is.EqualTo(array.Length));
        }

        [Test]
        public void TestCount()
        {
            Stack<int> stack = GetEmptyStack();
            Assert.That(stack.Count, Is.EqualTo(0));

            stack.Push(1);
            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestPush()
        {
            Stack<int> stack = GetEmptyStack();
            int len = 5;
            for (int i = 0; i < len; i++)
            {
                stack.Push(i);
            }
            Assert.That(stack.Count, Is.EqualTo(len));
            Assert.That(stack.Top, Is.EqualTo(len - 1));
        }

        [Test]
        public void TestPop()
        {
            Stack<int> stack = GetStackRangeOneToTen();
            Assert.That(stack.Count, Is.EqualTo(10));
            Assert.That(stack.Top, Is.EqualTo(10));

            int popped = stack.Pop();
            Assert.That(popped, Is.EqualTo(10));
            Assert.That(stack.Top, Is.EqualTo(9));
        }

        [Test]
        [ExpectedException(typeof(StackUnderflowException))]
        public void TestPop_EmptyStack()
        {
            Stack<int> stack = GetEmptyStack();
            stack.Pop();
        }

        [Test]
        public void TestTop()
        {
            Stack<int> stack = GetStackRangeOneToTen();

            Assert.That(stack.Top, Is.EqualTo(10));
            stack.Pop();
            Assert.That(stack.Top, Is.EqualTo(9));
        }

        [Test]
        [ExpectedException(typeof(StackUnderflowException))]
        public void TestTop_EmptyStack()
        {
            Stack<int> stack = GetEmptyStack();
            stack.Pop();
        }

        [Test]
        public void TestEnumerator()
        {
            Stack<int> stack = GetEmptyStack();
            foreach (int item in stack)
            {
                Console.WriteLine(item);
            }

            stack = GetStackRangeOneToTen();
            foreach (int item in stack)
            {
                Console.WriteLine(item);
            }
        }
    }
}
