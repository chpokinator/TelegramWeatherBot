using Newtonsoft.Json.Linq;
using OpenWeather;

namespace Tests.OpenWeatherTests;

public class СonverterTests
{
    [Fact]
        public void ToModel_ValidToken_ReturnsModel()
        {
            // Arrange
            var json = "{ \"Name\": \"John Doe\", \"Age\": 30 }";
            var token = JToken.Parse(json);
            // Act
            var result = token.ToModel<Person>();
            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal(30, result.Age);
        }

        [Fact]
        public void ToModel_NullToken_ReturnsNull()
        {
            // Arrange
            JToken? token = null;
            // Act
            var result = token.ToModel<Person>();
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToModelsList_ValidJArray_ReturnsModelList()
        {
            // Arrange
            var json = "[{ \"Name\": \"John Doe\", \"Age\": 30 }, { \"Name\": \"Jane Doe\", \"Age\": 25 }]";
            var jArray = JArray.Parse(json);

            // Act
            var result = jArray.ToModelsList<Person>();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("John Doe", result[0].Name);
            Assert.Equal(30, result[0].Age);
            Assert.Equal("Jane Doe", result[1].Name);
            Assert.Equal(25, result[1].Age);
        }

        [Fact]
        public void ToModelsList_NullJArray_ReturnsNull()
        {
            // Arrange
            JArray? jArray = null;

            // Act
            var result = jArray.ToModelsList<Person>();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ToModelsList_InvalidJArray_ReturnsNull()
        {
            // Arrange
            var json = "[]";
            var jArray = JArray.Parse(json);

            // Act
            var result = jArray.ToModelsList<Person>();

            // Assert
            Assert.Null(result);
        }

        private class Person
        {
            public string? Name { get; set; }
            public int Age { get; set; }
        }
}