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
    }
}