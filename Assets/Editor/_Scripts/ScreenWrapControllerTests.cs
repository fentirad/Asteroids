using NUnit.Framework;
using NSubstitute;
using UnityEngine;

[TestFixture]
public class ScreenWrapControllerTests {
	ScreenWrap screenWrap;
	ScreenBounds bounds;

	private ScreenWrap GetMockScreenWrap() {
		return Substitute.For<ScreenWrap>();
	}

	[SetUp]
	public void BeforeEach() {
		screenWrap = GetMockScreenWrap();

		bounds.top = 10f;
		bounds.bottom = -10f;
		bounds.right = 10f;
		bounds.left = -10f;

		screenWrap.Bounds = bounds;
	}

	[Test]
	public void ObjectOnScreen_WhenObjectInBounds() {
		Vector3 position = new Vector3(0f,0f,0f);

		Assert.True(screenWrap.ObjectOnScreen(position));
	}

	[Test]
	public void ObjectOnScreen_WhenObjectOutOfBounds() {
		Vector3 position = new Vector3(10f,0f,0f);

		Assert.False(screenWrap.ObjectOnScreen(position));
	}

	[Test]
	public void UpdatePosition_WhenObjectIsVisible() {
		Vector3 visiblePosition = new Vector3(0f,0f,0f);
		Vector3 newPosition = screenWrap.UpdatePosition(visiblePosition, Quaternion.identity);

		Assert.That(visiblePosition, Is.EqualTo(newPosition));
	}

	[Test]
	public void UpdatePosition_WhenObjectIsOffScreenInXDirection() {
		Vector3 visiblePosition = new Vector3(10f,0f,0f);
		Vector3 newPosition = screenWrap.UpdatePosition(visiblePosition, Quaternion.identity);

		Assert.AreNotEqual(visiblePosition, newPosition);
	}

	// [Test]
	// public void UpdatePosition_WhenObjectIsOffScreenInYDirection() {
	// 	Vector3 visiblePosition = new Vector3(0f,10f,0f);
	// 	Vector3 newPosition = screenWrap.UpdatePosition(visiblePosition, Quaternion.identity);

	// 	Assert.AreEqual(visiblePosition, newPosition);
	// 	Assert.That(visiblePosition, Is.EqualTo(newPosition));
	// }

}