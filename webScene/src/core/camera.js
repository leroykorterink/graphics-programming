const HALF_CIRCLE = Math.PI / 2;

const ROTATE_SPEED = 0.001;

class Camera {
  constructor() {
    this.handleKeydown = this.handleKeydown.bind(this);
    this.handleKeyup = this.handleKeyup.bind(this);
    this.handleMouseMove = this.handleMouseMove.bind(this);

    this.velocity = new THREE.Vector3();
    this.velocityScale = 0.1;

    this.camera = new THREE.PerspectiveCamera(
      75, // fov — Camera frustum vertical field of view.
      window.innerWidth / window.innerHeight, // aspect — Camera frustum aspect ratio.
      0.1, // near — Camera frustum near plane.
      1000
    ); // far — Camera frustum far plane.

    // Set rotation order to YXZ since rotation is not commutative
    this.camera.rotation.order = "YXZ";
    this.camera.position.set(11, 3.5, -8);
    this.camera.lookAt(8, 3, 4);

    // Event handlers
    const speedIndicator = document.querySelector(".speedIndicator");

    document.addEventListener("keydown", this.handleKeydown);
    document.addEventListener("keyup", this.handleKeyup);

    document.addEventListener("wheel", wheelEvent => {
      this.velocityScale = Math.min(
        0.5,
        Math.max(0.05, this.velocityScale + -(wheelEvent.deltaY / 10000))
      );

      speedIndicator.innerHTML = Math.trunc(this.velocityScale * 100);
    });

    document.addEventListener("pointerlockchange", event => {
      if (document.pointerLockElement) {
        document.addEventListener("mousemove", this.handleMouseMove);
      } else {
        document.removeEventListener("mousemove", this.handleMouseMove);
      }
    });
  }

  getCamera() {
    return this.camera;
  }

  update() {
    this.camera.translateX(this.velocity.x);
    this.camera.translateY(this.velocity.y);
    this.camera.translateZ(this.velocity.z);
  }

  handleKeydown(keydownEvent) {
    switch (keydownEvent.key) {
      case "W":
      case "w":
        this.velocity.setZ(-1 * this.velocityScale);
        break;

      case "A":
      case "a":
        this.velocity.setX(-1 * this.velocityScale);
        break;

      case "S":
      case "s":
        this.velocity.setZ(1 * this.velocityScale);
        break;

      case "D":
      case "d":
        this.velocity.setX(1 * this.velocityScale);
        break;

      case "Q":
      case "q":
        this.velocity.setY(-1 * this.velocityScale);
        break;

      case "E":
      case "e":
        this.velocity.setY(1 * this.velocityScale);
        break;
    }
  }

  handleKeyup(keyupEvent) {
    switch (keyupEvent.key) {
      case "s":
      case "S":
      case "w":
      case "W":
        this.velocity.setZ(0);
        break;

      case "a":
      case "A":
      case "d":
      case "D":
        this.velocity.setX(0);
        this.velocity.setX(0);
        break;

      case "q":
      case "Q":
      case "e":
      case "E":
        this.velocity.setY(0);
        this.velocity.setY(0);
        break;
    }
  }

  handleMouseMove(mousemoveEvent) {
    const newY =
      this.camera.rotation.y + -mousemoveEvent.movementX * ROTATE_SPEED;
    const newX =
      this.camera.rotation.x + -mousemoveEvent.movementY * ROTATE_SPEED;

    this.camera.rotation.y = newY;
    this.camera.rotation.x = Math.min(
      HALF_CIRCLE,
      Math.max(-HALF_CIRCLE, newX)
    );
  }
}

export default Camera;
