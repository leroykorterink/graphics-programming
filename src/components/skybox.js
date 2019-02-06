let size = 1000;

var geometry = new THREE.BoxGeometry(size, size, size);
var material = new THREE.MeshNormalMaterial({ color: 0x00ff00, side: THREE.BackSide });
var cube = new THREE.Mesh(geometry, material);

export default cube;
 