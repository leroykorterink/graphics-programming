using graphics_programming;
using Xunit;

namespace graphics_programming_tests
{
    public class Matrix4Tests
    {

        [Fact]
        public void Should_CheckEquality()
        {
            var m1 = new Matrix4(
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1
            );

            var m2 = new Matrix4(
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1
            );

            Assert.True(m1.Equals(m2));
            Assert.True(m1 == m2);
            Assert.False(m1 != m2);
        }

        [Fact]
        public void Should_AddMatrixes()
        {
            var m1 = new Matrix4(
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1
            );

            var m2 = new Matrix4(
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1
            );

            var expected = new Matrix4(
                2, 2, 2, 2,
                2, 2, 2, 2,
                2, 2, 2, 2,
                2, 2, 2, 2
            );

            Assert.Equal(expected, m1 + m2);
        }

        [Fact]
        public void Should_SubtractMatrixes()
        {
            var m1 = new Matrix4(
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1
            );

            var m2 = new Matrix4(
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1
            );

            var expected = new Matrix4(
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            );

            Assert.Equal(expected, m1 - m2);
        }

        [Fact]
        public void Should_MultiplyMatrixWithScalar()
        {
            var m1 = new Matrix4(
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1
            );

            var scalar = 2;

            var expected = new Matrix4(
                2, 2, 2, 2,
                2, 2, 2, 2,
                2, 2, 2, 2,
                2, 2, 2, 2
            );

            Assert.Equal(expected, m1 * scalar);
        }

        [Fact]
        public void Should_MultiplyMatrixes()
        {
            var m1 = new Matrix4(
                1, 2, 3, 4,
                2, 2, 3, 4,
                3, 3, 3, 4,
                4, 4, 4, 4
            );

            var m2 = new Matrix4(
                2, 2, 2, 2,
                2, 2, 2, 2,
                2, 2, 2, 2,
                2, 2, 2, 2
            );

            var expected = new Matrix4(
                20, 20, 20, 20,
                22, 22, 22, 22,
                26, 26, 26, 26,
                32, 32, 32, 32
            );

            Assert.Equal(expected, m1 * m2);
        }

        [Fact]
        public void Should_MultiplyMatrixWithVector()
        {
            var m1 = new Matrix4(
                1, 0, 0, -1,
                0, 1, 0, -2,
                0, 0, 1, -3,
                0, 0, 0, 1
            );

            var expected = new Vector3(-1, -2, -3);

            Assert.Equal(expected, m1 * new Vector3());
        }

        [Fact]
        public void Should_AddTranslationToMatrix()
        {
            var m1 = new Matrix4();
            m1.Translate(new Vector3(-1, -2, -3));

            var expected = new Matrix4(
                1, 0, 0, -1,
                0, 1, 0, -2,
                0, 0, 1, -3,
                0, 0, 0, 1
            );

            Assert.Equal(expected, m1);
        }

        [Fact]
        public void Should_RotateX()
        {
            var m1 = new Matrix4();
            m1.RotateX(45);

            var expected = new Matrix4(
                1, 0, 0, 0,
                0, 1.70710683F, -0.707106769F, 0,
                0, 0.707106769F, 1.70710683F, 0,
                0, 0, 0, 1
            );

            Assert.Equal(expected, m1);
        }

        [Fact]
        public void Should_RotateY()
        {
            var m1 = new Matrix4();
            m1.RotateY(45);

            var expected = new Matrix4(
                1.70710683F, 0, 0.707106769F, 0,
                0, 1, 0, 0,
                -0.707106769F, 0, 1.70710683F, 0,
                0, 0, 0, 1
            );

            Assert.Equal(expected, m1);
        }

        [Fact]
        public void Should_RotateZ()
        {
            var m1 = new Matrix4();
            m1.RotateZ(45);

            var expected = new Matrix4(
                1.70710683F, -0.707106769F, 0, 0,
                0.707106769F, 1.70710683F,  0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );

            Assert.Equal(expected, m1);
        }
    }
}
