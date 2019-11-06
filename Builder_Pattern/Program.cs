using System;
using System.Collections.Generic;
using System.Linq;

namespace Builder_Pattern
{
    class Program
    {
        class Element
        {
            string name;
            string value;

            public void SetName(string name)
            {
                this.name = name;
            }

            public string GetName()
            {
                return this.name;
            }

            public void SetValue(string value)
            {
                this.value = value;
            }

            public string GetValue()
            {
                return this.value;
            }
        }

        class Computer
        {
            private List<Element> elements;

            private void SetElement(string elementName, string monitor)
            {
                if (elements == null)
                {
                    elements = new List<Element>();
                }

                var element = elements.Exists(n => n.GetName().Equals(elementName)) ? elements.First(n => n.GetName().Equals(elementName)) : null;

                if (element != null)
                {
                    element.SetValue(monitor);
                }
                else
                {
                    element = new Element();
                    element.SetName(elementName);
                    element.SetValue(monitor);
                }

                elements.Add(element);
            }

            public void SetMonitor(string monitor)
            {
                SetElement("monitor", monitor);
            }

            public void SetProcessor(string processor)
            {
                SetElement("processor", processor);
            }

            public void SetGraphics(string graphics)
            {
                SetElement("graphics", graphics);
            }

            public void SetRam(string ram)
            {
                SetElement("ram", ram);
            }

            public void SetHdd(string hdd)
            {
                SetElement("hdd", hdd);
            }

            public List<Element> GetElements()
            {
                return elements;
            }
        }

        class SpecificationValidator
        {
            public void GetSpecification(Computer computer)
            {
                foreach (var elem in computer.GetElements())
                {
                    Console.WriteLine("Element {0} is {1}.", elem.GetName(), elem.GetValue());
                }
            }
        }

        abstract class Builder
        {
            protected Computer computer;

            public void NewComputer()
            {
                computer = new Computer();
            }

            public Computer GetComputer()
            {
                return computer;
            }

            public abstract void BuildMonitor();
            public abstract void BuildProcessor();
            public abstract void BuildGraphics();
            public abstract void BuildRam();
            public abstract void BuildHdd();
        }

        class XT001 : Builder
        {
            public override void BuildGraphics()
            {
                computer.SetGraphics("ATI");
            }

            public override void BuildHdd()
            {
                computer.SetHdd("Samsung 1 TB");
            }

            public override void BuildMonitor()
            {
                computer.SetMonitor("Benq 27");
            }

            public override void BuildProcessor()
            {
                computer.SetProcessor("AMD X1");
            }

            public override void BuildRam()
            {
                computer.SetRam("8 GB");
            }
        }

        class XT002 : Builder
        {
            public override void BuildGraphics()
            {
                computer.SetGraphics("NVidia");
            }

            public override void BuildHdd()
            {
                computer.SetHdd("Sagate 2 TB");
            }

            public override void BuildMonitor()
            {
                computer.SetMonitor("Dell 35");
            }

            public override void BuildProcessor()
            {
                computer.SetProcessor("Intel Y2");
            }

            public override void BuildRam()
            {
                computer.SetRam("16 GB");
            }
        }

        class Director
        {
            private Builder builder;

            public void SetBuilder(Builder builder)
            {
                this.builder = builder;
            }

            public Computer GetComputer()
            {
                return builder.GetComputer();
            }

            public void MakeComputer()
            {
                builder.NewComputer();
                builder.BuildGraphics();
                builder.BuildHdd();
                builder.BuildMonitor();
                builder.BuildProcessor();
                builder.BuildRam();
            }
        }

        static void Main(string[] args)
        {
            Director director = new Director();
            Builder builder1 = new XT001();
            Builder builder2 = new XT002();

            Console.WriteLine("Zestaw1");
            director.SetBuilder(builder1);
            director.MakeComputer();

            Computer computer1 = director.GetComputer();
            SpecificationValidator validator = new SpecificationValidator();
            validator.GetSpecification(computer1);

            Console.WriteLine("\nZestaw2");
            director.SetBuilder(builder2);
            director.MakeComputer();

            Computer computer2 = director.GetComputer();
            validator.GetSpecification(computer2);

            Console.ReadKey();
        }
    }
}
