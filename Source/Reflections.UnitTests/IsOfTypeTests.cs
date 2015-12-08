﻿using FluentAssertions;

using NUnit.Framework;

using Reflections.UnitTests.TestClasses;

namespace Reflections.UnitTests
{
    [TestFixture]
    public class IsOfTypeTests
    {
        [Test]
        public void IsNotOfTypeReturnsFalseWhenExtendedTypeIsDerivedFromTypeArgumentType()
        {
            // Arrange
            var extendedTypeClass = typeof(DerivedClass);

            // Act
            var result = extendedTypeClass.IsNotOfType<BaseClass>();

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsNotOfTypeReturnsTrueWhenExtendedTypeAndTypeArgumentTypeAreNotDerivedFromEachOther()
        {
            // Arrange
            var extendedClassType1 = typeof(UnrelatedClassOne);
            var extendedClassType2 = typeof(UnrelatedClassTwo);

            // Act
            var result1 = extendedClassType1.IsNotOfType<UnrelatedClassTwo>();
            var result2 = extendedClassType2.IsNotOfType<UnrelatedClassOne>();

            // Assert
            result1.Should().BeTrue();
            result2.Should().BeTrue();
        }

        [Test]
        public void IsNotOfTypeReturnsTrueWhenExtendedTypeIsNotDerivedFromTypeArgumentType()
        {
            // Arrange
            var extendedTypeClass = typeof(BaseClass);

            // Act
            var result = extendedTypeClass.IsNotOfType<DerivedClass>();

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsOfTypeReturnsFalseWhenExtendedTypeAndTypeArgumentTypeAreNotDerivedFromEachOther()
        {
            // Arrange
            var extendedClassType1 = typeof(UnrelatedClassOne);
            var extendedClassType2 = typeof(UnrelatedClassTwo);

            // Act
            var result1 = extendedClassType1.IsOfType<UnrelatedClassTwo>();
            var result2 = extendedClassType2.IsOfType<UnrelatedClassOne>();

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Test]
        public void IsOfTypeReturnsFalseWhenExtendedTypeIsNotDerivedFromTypeArgumentType()
        {
            // Arrange
            var extendedTypeClass = typeof(BaseClass);

            // Act
            var result = extendedTypeClass.IsOfType<DerivedClass>();

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsOfTypeReturnsTrueWhenExtendedTypeIsDerivedFromTypeArgumentType()
        {
            // Arrange
            var extendedTypeClass = typeof(DerivedClass);

            // Act
            var result = extendedTypeClass.IsOfType<BaseClass>();

            // Assert
            result.Should().BeTrue();
        }
    }
}