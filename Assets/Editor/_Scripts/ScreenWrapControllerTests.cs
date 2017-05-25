using NUnit.Framework;
using NSubstitute;
using UnityEngine;

[TestFixture]
public class ScreenWrapControllerTests {
	IScreenWrap screenWrap;
	ScreenBounds bounds;

	private IScreenWrap GetMockScreenWrap() {
		return Substitute.For<IScreenWrap>();
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

		screenWrap.ObjectOnScreen(position).Returns(true);

		Assert.True(screenWrap.ObjectOnScreen(position));
	}

	[Test]
	public void ObjectOnScreen_WhenObjectOutOfBounds() {
		Vector3 position = new Vector3(10.1f,0f,0f);

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
		// requires methods that require mocking to be virtual
		screenWrap = Substitute.ForPartsOf<ScreenWrap>();
		screenWrap.Bounds = bounds;

		Vector3 visiblePosition = new Vector3(10.1f,0f,0f);
		Quaternion visibleRotation = Quaternion.identity;
		Vector3 expectedPosition = new Vector3(-9.9f,0f,0f);
		
		//setup mock
		screenWrap.When(x => x.SwapEntities(visiblePosition, visibleRotation)).DoNotCallBase();
		screenWrap.SwapEntities(visiblePosition, visibleRotation).Returns(expectedPosition);
		
		// alternative setup mock
		// screenWrap.SwapEntities(Arg.Is(visiblePosition), Arg.Is(visibleRotation)).Returns(expectedPosition);
		
		Vector3 newPosition = screenWrap.UpdatePosition(visiblePosition, visibleRotation);
		
		Assert.That(newPosition, Is.EqualTo(expectedPosition));
	}
}