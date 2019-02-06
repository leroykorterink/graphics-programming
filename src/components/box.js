var geometry = new THREE.BoxGeometry(1, 1, 1);
var material = new THREE.MeshNormalMaterial({ color: 0x00ff00 });
var cube = new THREE.Mesh(geometry, material);

cube.position.y = (cube.scale.y / 2) + 0.001

let velocity = -0.005;

export const update = () => {
  if (velocity >= 0 && cube.position.x > 2) velocity = -0.005;
  if (velocity < 0 && cube.position.x < -2) velocity = 0.005;

  cube.position.set(cube.position.x + velocity, cube.position.y, 0);
};

export default cube;
