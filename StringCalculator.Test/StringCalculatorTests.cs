using System;
using FluentAssertions;
using NUnit.Framework;

namespace StringCalculator.Test
{
    public class StringCalculatorTests
    {
        Calculator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Calculator();
        }

        [Test]
        public void Add_EmptyString_return_Zero()
        {

            var result = _sut.Add(string.Empty);
            
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Add_with_a_single_number_return_the_number_itself()
        {
            var result = _sut.Add("1");
            
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Add_with_two_numbers_return_their_Sum()
        {
            var result = _sut.Add("1,2");
            
            Assert.That(result, Is.EqualTo(3));
        }
        
        [Test]
        public void Add_accept_sequences_of_number_of_any_amount()
        {
            var result = _sut.Add("1,2,3,4,5,6");
            
            Assert.That(result, Is.EqualTo(21));
        }

        [Test]
        public void Add_accept_also_NewLines_for_separator()
        {
            var result = _sut.Add("1\n2,3");
            
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void the_delimiter_can_be_specified_in_the_beginning_of_the_string()
        {
            var result = _sut.Add("//;\n1;2");
            
            Assert.That(result, Is.EqualTo(3));
        }
        
        [Test]
        public void Adding_negative_numbers_cause_a_specific_exception()
        {
            _sut.Invoking(x => x.Add("1,-2"))
                .Should().Throw<ArgumentException>()
                .WithMessage("Negative numbers are not allowed: -2");
        }
    
        [Test]
        public void The__exception_Adding_negative_numbers_contains_the_list_of_all_negative_Numbers_passed()
        {
            _sut.Invoking(x => x.Add("1,-2,-4,3,-7"))
                .Should().Throw<ArgumentException>()
                .WithMessage("Negative numbers are not allowed: -2,-4,-7");
        }

        [Test]
        public void GetCalledCount_return_the_number_of_Add_method_is_called()
        {
            _sut.Add("1");
            _sut.Add("2");
            _sut.Add("3");
            
            _sut.GetCalledCount().Should().Be(3);

        }

        [Test]
        public void AddOccured_Event_is_triggers_after_each_Add()
        {
            using var monitor = _sut.Monitor();
            
            _sut.Add("1");

            monitor.Should().Raise("AddOccurred");
        }

        [Test]
        public void numbers_bigger_than_1000_are_ignored()
        {
            _sut.Add("1,1001,2,2002").Should().Be(3);
        }
        
        [Test]
        public void the_delimiter_can_be_of_any_size()
        {
            var result = _sut.Add("//[***]\n1***2***3");
            
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void multiple_delimiters_can_be_specified()
        {
            var result = _sut.Add("//[*][!]\n1*2!3");
            
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void delimiters_with_variable_size_can_be_specified()
        {
            var result = _sut.Add("//[**][%%]\n1**2%%3");
            
            Assert.That(result, Is.EqualTo(6));
        }

    }
}