#r "mscorlib.dll"
#r "D:\home\Documents\Visual Studio 2015\Projects\CsCmds\CsCmds\bin\Debug\CsCmds.dll"

using CsCmds.Core;
using CsCmds.Dag;
using Autodesk.Maya.OpenMaya;

File.NewScene(true);

var persp = DependNode.FirstOrDefault(node => node.name == "persp");
Log.WriteLine(persp.Name + " " + persp.ApiType);
// -> // persp kTransform //

Mel.Evaluate("polySphere");
var sphere = DagNode.FirstOrDefault(node => node.name.StartsWith("pSphere"));
Log.WriteLine(sphere.Name);
// -> // pSphere1 //

foreach (var childNode in sphere.EnumerateChildren())
{
    Log.WriteLine(childNode.Name);
}
// -> // pSphereShape1 //

var sphereShape = sphere.FirstChildOrDefault(node => node.hasFn(MFn.Type.kShape));
Log.WriteLine(sphereShape.Name);
// -> // pSphereShape1 //

var t = sphere.FindPlug("translate")?.GetDataHandle().asFloatVector;
Log.WriteLine("{0}, {1}, {2}", t.x, t.y, t.z);
// -> // 0, 0, 0 // 

var modifier = new MDGModifier();
sphere.FindPlug("tx").SetValue<float>(1.0f, modifier);
modifier.doIt();

