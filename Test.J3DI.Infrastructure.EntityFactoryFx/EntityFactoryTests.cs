using FluentAssertions;
using J3DI.Domain;
using J3DI.Infrastructure.EntityFactoryFx;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Test.J3DI.Common;
using Test.J3DI.Domain;
using Test.J3DI.Infrastructure.EntityFactoryFx;
using Xunit;
using Xunit.Abstractions;


namespace Test.J3DI.Infrastructure.EntityFactoryFx {

    public class EntityFactoryTests 
    {
        private readonly ITestOutputHelper _testOutput;
        private readonly ILogger _logger;
        private readonly Mock<ILogger<EntityFactoryTests>> _mockLogger;

        public EntityFactoryTests(ITestOutputHelper output)
        {
            _testOutput = output;
            _mockLogger = (Mock<ILogger<EntityFactoryTests>>)LoggerUtils.LoggerMock<EntityFactoryTests>();
            _logger = _mockLogger.Object;
        }


        [Fact]
        public void can_build_entity()
        {
            var e1Factory = new EntityByString1Factory(_logger);
            e1Factory.ThrowExceptionOnNullDataReader = false;
            
            EntityBase<string> e1 = e1Factory.BuildEntity(null);

            e1.Id.Split('_')[0].Should().Be("EntityByString1");

            _mockLogger.VerifyLog(Microsoft.Extensions.Logging.LogLevel.Trace, "BuildEntit", Moq.Times.AtLeastOnce());
        }


        [Fact]
        public void can_build_entities()
        {
            var e1Factory = new EntityByString1Factory(_logger);
            e1Factory.ThrowExceptionOnNullDataReader = false;
            
            IEnumerable<EntityBase<string>> e1List = e1Factory.BuildEntities(null);

            e1List.Should().NotBeEmpty();

            _mockLogger.VerifyLog(Microsoft.Extensions.Logging.LogLevel.Trace, "BuildEntit", Moq.Times.AtLeastOnce());
        }


        [Fact]
        public void verify_excp_on_null_datareader()
        {
            EntityByString1 e1 = null;
            Exception ex = Assert.Throws<ArgumentNullException>(
                () => e1 = (EntityByString1)(new EntityByString1Factory(_logger)).BuildEntity(null)
            );

            Assert.Contains("record", ex.Message);

            _mockLogger.VerifyLog(Microsoft.Extensions.Logging.LogLevel.Trace, "BuildEntity", Moq.Times.AtLeastOnce());
            _mockLogger.VerifyLog(Microsoft.Extensions.Logging.LogLevel.Error, "BuildEntity", Moq.Times.AtLeastOnce());
        }



        [Fact]
        public void verify_correct_type_from_EntityType()
        {
            Assert.Equal(
                typeof(EntityByString1),
                (new EntityByString1Factory().EntityType())
            );

            Assert.Equal(
                typeof(EntityByString2),
                (new EntityByString2Factory().EntityType())
            );
        }

    }
}