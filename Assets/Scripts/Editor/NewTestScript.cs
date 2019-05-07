using System.Collections;
using System.Collections.Generic;
using HabitApp;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions

            var task = new TaskData()
            {
            };

            var json = JsonConvert.SerializeObject(task);
            
            Debug.Log(json);

            task = JsonConvert.DeserializeObject<TaskData>(json);
            
            Debug.Log(task.Status);
            
            Assert.IsTrue(true);

        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
