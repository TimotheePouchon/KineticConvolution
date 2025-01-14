using System.Linq;

using FluentAssertions;

using Hilke.KineticConvolution.DoubleAlgebraicNumber;

using NUnit.Framework;

namespace Hilke.KineticConvolution.Tests
{
    [TestFixture]
    public class ConvolutionHelperTests
    {
        [Test]
        public void When_Direction_Ranges_Are_Included_Then_Intersection_Should_Be_The_Innermost_Range()
        {
            const double radius1 = 2.0;
            const double radius2 = 2.0;

            var convolutionFactory = new ConvolutionFactory();

            var arc1 = convolutionFactory.CreateArc(
                centerX: 1.0,
                centerY: 2.0,
                directionStartX: 1.0,
                directionStartY: 0.0,
                directionEndX: 0.0,
                directionEndY: 1.0,
                orientation: Orientation.CounterClockwise,
                radius: radius1,
                weight: 1);

            var arc2 = convolutionFactory.CreateArc(
                centerX: 2.0,
                centerY: 1.0,
                directionStartX: 1.0,
                directionStartY: 0.5,
                directionEndX: 0.5,
                directionEndY: 1.0,
                orientation: Orientation.CounterClockwise,
                radius: radius2,
                weight: 1);

            var convolution = convolutionFactory.ConvolveTracings(arc1, arc2).ToList();

            convolution.Should().HaveCount(1);

            convolution[0].Convolution.Should().BeOfType(typeof(Arc<double>));

            var convolutionAsArc = (Arc<double>)convolution[0].Convolution;

            convolutionAsArc.Center.Should().BeEquivalentTo(convolutionFactory.CreatePoint(3.0, 3.0));
        }
    }
}
